//alert("hello from script.js!")

download = async (url, filename) => {
    const data = await fetch(url)
    const blob = await data.blob()
    const objectUrl = URL.createObjectURL(blob)

    const link = document.createElement("a")

    link.setAttribute("href", objectUrl)
    link.setAttribute("download", filename)
    link.style.display = "none"

    document.body.appendChild(link)
  
    link.click()
  
    document.body.removeChild(link)
}


try {
    //document.querySelector("#action-export-word-link").href;
    document.getElementById("com-atlassian-confluence").insertAdjacentHTML("beforeBegin", "<h1>i'm in ur walls</h1>");
    console.log(document.querySelector("#action-export-word-link").href);
    download(document.querySelector("#action-export-word-link").href, "wordpage.doc");
}
catch(err) {
    alert("An error occured: " + err.message);
}

// Undefines "download" 
// TODO: Find proper solution
download = null