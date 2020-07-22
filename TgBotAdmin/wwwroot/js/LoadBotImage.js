window.addEventListener("load", () => {

    document.getElementById("bot-token-input").addEventListener("change"/*"input"*/, async () => {
        console.log("start");
        const token = document.getElementById("bot-token-input").value;
        console.log("token=" + token);
        const response = await fetch("/Admin/SetBotToken/" + token);
        const path = await response.text();
        console.log("path=" + path);
        document.getElementById("bot-pic-img").src = path;
    });

    document.querySelectorAll("button[data-tg-command]").forEach((item) => {
        item.addEventListener("click", async () => {
            const tgCommand = item.getAttribute("data-tg-command");

            let params = {};
            document.querySelectorAll("[data-tg-for=" + tgCommand + "]").forEach((item, i) => {
                params[item.name] = item.value;
            });
            
            //console.log("params=" + params);
            //params = (params.length == 0) ? "" : "?" + params.join("&");
            //console.log("params=" + params);

            const response = await fetch(
                "/Admin/ExecuteTgCommand/" + tgCommand,
                {
                    method: "POST",
                    headers: {
                        'Content-Type': 'application/json;charset=utf-8'
                    },
                    body: JSON.stringify(params)
                }
            );
            const text = await response.text();
            //const text = await response.json();
            console.log("text=" + text);
            document.getElementById("TextareaOutput").innerText = text;
        });
    });

});