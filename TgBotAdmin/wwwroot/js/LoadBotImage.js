window.addEventListener("load", () => {

    document.getElementById("bot-token-input").addEventListener("change"/*"input"*/, async () => {
        console.log("start");
        const token = document.getElementById("bot-token-input").value;
        console.log("token=" + token);
        let response = await fetch("/Admin/SetBotToken/" + token);
        let path = await response.text();
        console.log("path=" + path);
        document.getElementById("bot-pic-img").src =
            "https://api.telegram.org/file/bot" + token + "/" + path;
    });

    document.querySelectorAll("button[data-tg-command]").forEach((item) => {
        item.addEventListener("click", async () => {
            console.log("/Admin/ExecuteTgCommand/" + item.getAttribute("data-tg-command"));
            let response = await fetch("/Admin/ExecuteTgCommand/" + item.getAttribute("data-tg-command"));
            let text = await response.text();
            console.log("text=" + text);
            document.getElementById("TextareaOutput").innerText = text;
        });
    });

});