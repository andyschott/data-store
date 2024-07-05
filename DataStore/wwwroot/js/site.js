// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function getValue() {
    const input = document.getElementById('key');
    const key = input.value;
    
    const http = new XMLHttpRequest();
    http.onreadystatechange = function() {
        if (http.readyState === 4 && http.status === 200) {
            const output = document.getElementById('output');
            const response = JSON.parse(http.responseText);

            output.innerText = response.value;
        }
    }
    const url = `http://localhost:8080/api/values/1/${key}`;
    http.open('GET', url);
    http.send(null);
}