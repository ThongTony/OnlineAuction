﻿/// <reference path="../lib/signalr/browser/signalr.js" />
// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(() => {
    let connection = new signalR.HubConnectionBuilder().withUrl("/signalRHub").build();

    connection.start();

    connection.on("refreshNotifications", function () {
        loadData();
    });

    loadData();

    function loadData() {
        var tr = '';

        $.ajax({
            url: '/NotifiedExpiredItem/GetExpiredItems',
            method: 'GET',
            success: (result) => {
                $.each(result, (k, v) => {
                    var isAvailable = v.isAvailable ? 'available' : 'not available';
                    var className = v.isAvailable ? 'badge badge-success' : 'not badge badge-warning';
                    tr = tr + `<tr style="width:90px;">
                        <td style="width:90px;">${v.itemId}</td>
                        <td style="width:90px;">${v.currentDate}</td>
                        <td style="width:90px;">${v.isExpired}</td>
                        <td style="width:90px;">${v.isSeen}</td>
                    </tr>`;
                });

                $("#tableBody").html(tr);
            },
            error: (error) => {
                console.log(error);
            }
        });
    }
});