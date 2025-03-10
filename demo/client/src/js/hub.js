"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:5001/kafka/hubService").build();

connection.on("RelayMessage", function (topicName,message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    Component.TopicList.topics.forEach(topic => {
        if (topic.Topic === topicName) {
            topic.Messages = topic.Messages || [];
            topic.Messages.push(msg);
            
            topic.changeCount(topic);
        }
    });
});


connection.start().then(function () {
    Component.TopicList.topics = [{
        Topic:"MyTopic",
        "Messages":[]
    },
    {
        Topic:"MyTopic1",
        "Messages":[]
    }];
    connection.invoke("Subscribe", "MyTopic");
    connection.invoke("Subscribe", "MyTopic1");
}).catch(function (err) {
    return console.error(err.toString());
});