window.addEventListener("load", () => {

    document.getElementById("bot-token-input").addEventListener("change"/*"input"*/, async () => {
        console.log("start");
        const token = document.getElementById("bot-token-input").value;
        alert(token);
        console.log(token);
        let response = await fetch("/Admin/GetBotPictLink/" + token);
        let path = await response.text();
        alert("https://api.telegram.org/file/bot" + token + path);
        document.getElementById("bot-pic-img").src =
            "https://api.telegram.org/file/bot" + token + "/" + path;
    });

});