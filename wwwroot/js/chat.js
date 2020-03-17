"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    if (user.length === 0) {
        user = "Unknown";
    }
    if (msg.length === 0) {
        msg = "Nothing";
    }
    var encodedUser = user + " - ";
    var encodedMessage = msg;
    var b = document.createElement("b");
    var div = document.createElement("div");
    b.textContent = encodedUser;
    div.textContent = encodedMessage;
    div.classList.add("alert");
    div.classList.add("alert-success");
    div.prepend(b);
    document.getElementById("messagesList").appendChild(div);

});

connection.on("ReceiveHistory", function (messages) {
    messages.forEach(m => {
        var div = document.createElement("div");
        div.innerHTML = m.message;
        document.getElementById("messagesList").appendChild(div);
    })
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

document.getElementById("historyButton").addEventListener("click", function (event) {
    connection.invoke("DisplayHistory").catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});