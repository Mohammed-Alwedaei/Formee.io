const popupButton = document.querySelector(".fm-livechat__popup-button");
const registerButton = document.querySelector(".fm-livechat__register-button");
const registerTab = document.querySelector(".fm-livechat__register");

const wrapper = document.querySelector(".fm-livechat__wrapper");

popupButton.addEventListener("click", () => {
  if (!wrapper.classList.contains("reveal")) {
    wrapper.classList.add("reveal");
  } else {
    wrapper.classList.remove("reveal");
  }
});

registerButton.addEventListener("click", () => {
  var fullname = document.querySelector("#fullname").value;
  var email = document.querySelector("#email").value;

  startSession(fullname, email);
});

function startSession(fullname, email) {
  var connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:7270/hubs/livechat")
    .build();

  connection.start();

  connection.invoke("StartSession", fullname, email).catch(function (err) {
    return console.error(err.toString());
  });
}
