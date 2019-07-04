(function (signalR) {
    "use strict";

    var btnChangeStateElem = document.getElementById("btnChangeState");
    var messagesListElem = document.getElementById("messagesList");
    var deviceIdElem = document.getElementById("deviceId");
    var deviceNameElem = document.getElementById("deviceName");

    function safeHtml(message) {
        var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
        return msg;
    }

    function removeChild(elem) {
        while (elem.hasChildNodes()) {
            elem.removeChild(elem.childNodes[0]);
        }
    }

    var connection = new signalR.HubConnectionBuilder().withUrl("/Hubs/DeviceHub").build();
    btnChangeStateElem.disabled = true;

    connection.on("UpdateDeviceStates", function (devices, device) {

        console.log("UpdateDeviceStates");
        console.log(devices);
        console.log(device);

        removeChild(messagesListElem);

        for (var i = 0; i < devices.length; i++) {
            var theDevice = devices[i];
            var msg = theDevice.id + ", " + theDevice.name + ": " + theDevice.deviceStateCode;
            var encodedMsg = safeHtml(msg);
            var li = document.createElement("li");
            if (device) {
                if (theDevice.id === device.id) {
                    console.log("same id: " + theDevice.id);
                    encodedMsg = "-> " + encodedMsg;
                } 
            }
            li.textContent = encodedMsg;
            if (theDevice.deviceStateCode !== "Offline") {
                li.className = "text-info";
            } else {
                li.className = "text-danger";
            }
            messagesListElem.appendChild(li);
        }
    });

    connection.start().then(function () {
        btnChangeStateElem.disabled = false;
    }).catch(function (err) {
        return console.error(err.toString());
    });

    btnChangeStateElem.addEventListener("click", function (event) {
        var deviceId = deviceIdElem.value;
        var deviceName = deviceNameElem.value;
        if (!deviceId || !deviceName) {
            alert("bad id or name!");
            return;
        }

        var btnChangeStateValue = btnChangeStateElem.value;
        var method = null;
        if (btnChangeStateValue === "-> Online") {
            method = "Online";
            btnChangeStateElem.value = "-> Offline";
        } else {
            method = "Offline";
            btnChangeStateElem.value = "-> Online";
        }

        var device = {
            Id: deviceId,
            Name: deviceName
        }
        console.log("invoke method: " + method);
        connection.invoke(method, device).catch(function (err) {
            return console.error(err.toString());
        });
        event.preventDefault();
    });
}(signalR));