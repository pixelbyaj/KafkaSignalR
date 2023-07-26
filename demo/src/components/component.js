var Component = Component || {};
Component.TopicList = new Vue({
    el: "#topics",
    data: {
        topics: []
    },
    methods: {
        topicDetail: function(topic) {
            Component.TopicMessageList.Messages = topic.Messages;
            Component.TopicMessageList.seen = topic.Messages.length > 0;
        }
    },
    computed: {
        changeCount: function(topic) {
            topic.MessageCount = topic.Messages.length;
        }
    }
});
Component.TopicMessageList = new Vue({
    el: "#messageList",
    data: {
        Messages: [],
        seen: false
    }
});