"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (message) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    li.textContent = `${message}`;
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    fetch('Task/GetTasks')
            .then(response => response.json())
            .then(data => _displayItems(data))
            .catch(error => console.error('Unable to get items.', error));
    
});

function _displayItems(data) {
    for (let key in data) {
            let row = document.createElement('tr')
            row.innerHTML = `<td>${data[key].name}</td>
                             <td>${data[key].id}</td>
                             <td>${data[key].memory}</td>
                             <td>${data[key].startTime}</td>
                     `
            document.querySelector('.product').appendChild(row)
    }
}