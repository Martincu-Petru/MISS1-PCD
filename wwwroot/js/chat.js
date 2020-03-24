"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
var receivedHistory = false;

connection.on("ReceiveMessage", function (user, message) {
    var userLanguage = navigator.language;
    var googleApiLanguageCode = userLanguage.split('-')[0];
    connection.invoke("TranslateText", message, googleApiLanguageCode).then(function (translatedMessage) {
        var encodedUsernameText = user + " - ";
        var encodedMessageText = translatedMessage;
        var usernameText = document.createElement("b");
        var messageText = document.createElement("div");

        usernameText.textContent = encodedUsernameText;
        messageText.textContent = encodedMessageText;
        messageText.classList.add("alert");
        messageText.classList.add("alert-success");
        messageText.prepend(usernameText);

        document.getElementById("messagesList").appendChild(messageText);
    });
});

connection.on("ReceiveHistory", function (messages) {
    if (!receivedHistory) {
        messages.forEach(message => {
            var usernameText = document.createElement("b");
            var messageText = document.createElement("div");

            usernameText.textContent = message.user + " - ";
            messageText.textContent = message.text;
            messageText.classList.add("alert");
            messageText.classList.add("alert-success");
            messageText.prepend(usernameText);

            document.getElementById("messagesList").appendChild(messageText);
            receivedHistory = true;
        });
    }
});


document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("usernameInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err);
    });
    event.preventDefault();
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
    var userLanguage = navigator.language;

    var googleApiLanguageCode = userLanguage.split('-')[0] ;
    connection.invoke("DisplayHistory", googleApiLanguageCode).then(function (messages) {
        messages.forEach(message => {
            var usernameText = document.createElement("b");
            var messageText = document.createElement("div");

            usernameText.textContent = message.user + " - ";
            messageText.textContent = message.text;
            messageText.classList.add("alert");
            messageText.classList.add("alert-success");
            messageText.prepend(usernameText);

            document.getElementById("messagesList").appendChild(messageText);
        });
    });
}).catch(function (err) {
    return console.error(err);
});



