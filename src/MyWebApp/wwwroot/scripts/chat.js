"use strict";

var sendButtonElem = document.getElementById("sendButton");
var messagesListElem = document.getElementById("messagesList");
var userInputElem = document.getElementById("userInput");
var messageInputElem = document.getElementById("messageInput");

var connection = new signalR.HubConnectionBuilder().withUrl("/Hubs/ChatHub").build();
//Disable send button until connection is established
sendButtonElem.disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + " says " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    messagesListElem.appendChild(li);
});

connection.start().then(function () {
    sendButtonElem.disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

sendButtonElem.addEventListener("click", function (event) {
    var user = userInputElem.value;
    var message = messageInputElem.value;
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});