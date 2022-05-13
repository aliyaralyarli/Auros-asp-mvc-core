// window.onscroll = function() {myFunction()};

var prevScrollpos = window.pageYOffset;
window.onscroll = function () {
  var currentScrollPos = window.pageYOffset;
  if (prevScrollpos < currentScrollPos) {
    document.getElementById("up").style.display = "block";
  } else if (prevScrollpos > currentScrollPos) {
    document.getElementById("up").style.top = "none";
  }
  prevScrollpos = currentScrollPos;
};

const countdown = () => {
  const countDate = new Date("May 31,2022 00:00:00").getTime();
  const now = new Date().getTime();
  const difference = countDate - now;

  const second = 1000;
  const minute = second * 60;
  const hour = minute * 60;
  const day = hour * 24;

  const textDay = Math.floor(difference / day);
  const textHour = Math.floor((difference % day) / hour);
  const textMinute = Math.floor((difference % hour) / minute);
  const textSecond = Math.floor((difference % minute) / second);

  document.querySelector(".day").innerText = textDay;
  document.querySelector(".hour").innerText = textHour;
  document.querySelector(".minute").innerText = textMinute;
  document.querySelector(".second").innerText = textSecond;
};

setInterval(countdown, 1000);


setTimeout(() => { document.querySelector(".message").style.display = "none" }, 5000);

const sign_in_btn = document.querySelector("#sign-in-btn");
const sign_up_btn = document.querySelector("#sign-up-btn");
const container = document.querySelector(".container");

sign_up_btn.addEventListener("click", () => {
    container.classList.add("sign-up-mode");
});

sign_in_btn.addEventListener("click", () => {
    container.classList.remove("sign-up-mode");
});

function myFunction() {
    var x = document.getElementsByClassName("pass");
    if (x.type === "password") {
        x.type = "text";
    } else {
        x.type = "password";
    }
}