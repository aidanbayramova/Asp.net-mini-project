"use strict"

document.addEventListener("DOMContentLoaded", function () {
    const form = document.querySelector(".subscribe-form");

    form.addEventListener("submit", function (e) {
        e.preventDefault(); 

        const email = document.querySelector(".email").value; 

        if (!email || !email.includes("@")) {
            alert("Please enter a valid email address.");
            return;
        }

      
        const formData = new FormData();
        formData.append("email", email);

      
        fetch("/Newsletter/Subscribe", {
            method: "POST",
            headers: {
                'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
            },
            body: formData
        })
            .then(response => response.json()) 
            .then(data => {
                if (data.success) {
                  
                    const successDiv = document.createElement('div');
                    successDiv.classList.add('alert', 'alert-success', 'text-center');
                    successDiv.textContent = data.message;
                    form.appendChild(successDiv); 

                    document.querySelector(".email").value = ''; 
                } else {
               
                    const errorDiv = document.createElement('div');
                    errorDiv.classList.add('alert', 'alert-danger', 'text-center');
                    errorDiv.textContent = data.message;
                    form.appendChild(errorDiv);
                }
            })
            .catch(error => {
              
                alert("Something went wrong. Please try again later.");
            });
    });
});
