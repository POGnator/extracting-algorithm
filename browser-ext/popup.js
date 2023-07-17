//document.getElementById("hellobutton").addEventListener("click", hello);

function getTabID() {
  return new Promise((resolve, reject) => {
      try {
          chrome.tabs.query({
              active: true,
          }, function (tabs) {
              resolve(tabs[0].id);
          })
      } catch (e) {
          reject(e);
      }
  })
}

async function injectScript() {
  let responseTabID = await getTabID();

  document.getElementById("injectScript").addEventListener("click", function() {
    chrome.scripting
    .executeScript({
      target : {tabId : responseTabID},
      files : [ "script.js" ],
    })
    .then(() => console.log("script injected"));
  });
}

document.getElementById("getHelp").addEventListener("click", function() {
  chrome.tabs.create({
    url: "help.html"
  });
});

injectScript()


//"document.querySelector("#action-export-word-link").href"