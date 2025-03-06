document.getElementById("send-btn").addEventListener("click", function () {
    let userInput = document.getElementById("user-input").value;
    if (userInput.trim() === "") return;

    let chatBox = document.getElementById("chat-box");

    let userMessage = document.createElement("div");
    userMessage.textContent = "You: " + userInput;
    chatBox.appendChild(userMessage);

    document.getElementById("user-input").value = "";

    setTimeout(() => {
        let botMessage = document.createElement("div");
        botMessage.textContent = "Bot: " + "Hello! How can I assist you?";
        chatBox.appendChild(botMessage);
        chatBox.scrollTop = chatBox.scrollHeight;
    }, 500);
});
