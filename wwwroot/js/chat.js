"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

connection.on("ReceiveMessage", function (user, message) {
    var encodedUsernameText = user + " - ";
    var encodedMessageText = message;
    var usernameText = document.createElement("b");
    var messageText = document.createElement("div");

    usernameText.textContent = encodedUsernameText;
    messageText.textContent = encodedMessageText;
    messageText.classList.add("alert");
    messageText.classList.add("alert-success");
    messageText.prepend(usernameText);

    document.getElementById("messagesList").appendChild(messageText);

});

connection.on("ReceiveHistory", function (messages) {
    messages.forEach(message => {
        var usernameText = document.createElement("b");
        var messageText = document.createElement("div");
        console.log(message);
        usernameText.textContent = message.user + " - ";
        messageText.textContent = message.text;
        messageText.classList.add("alert");
        messageText.classList.add("alert-success");
        messageText.prepend(usernameText);

        document.getElementById("messagesList").appendChild(messageText);
    });
});


document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("usernameInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
    connection.invoke("DisplayHistory").catch(function (messages) {
        console.log(messages);
        messages.forEach(message => {
            var usernameText = document.createElement("b");
            var messageText = document.createElement("div");
            console.log(message);
            usernameText.textContent = message.user + " - ";
            messageText.textContent = message.text;
            messageText.classList.add("alert");
            messageText.classList.add("alert-success");
            messageText.prepend(usernameText);

            document.getElementById("messagesList").appendChild(messageText);
        });
    });
}).catch(function (err) {
    return console.error(err.toString());
});



