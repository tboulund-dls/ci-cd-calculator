// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {

	$("#execute").click(function () {
		$.ajax({
			type: "GET",
			url: window.location.protocol + "//" + window.location.hostname + ":8001/calculator",
			data: { 
				calculator: $("#calculator").val(),
				op: $("#operator").val(),
				number1: $("#number1").val(),
				number2: $("#number2").val()
			},
			success: function (data) {
				$("#result").val(data);
			}
		});
	});

});