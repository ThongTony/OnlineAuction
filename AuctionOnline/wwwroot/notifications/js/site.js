/// <reference path="../lib/signalr/browser/signalr.js" />
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
                    var className = !v.isSeen ? 'linebg' : '';
                    tr = tr + `<li class="notifiedline ${className}">
                            <a href="/Item/Details/${v.itemId}" role="button" tabindex="0">
                                <div>
                                    <div style="float:left;margin-right:10px;">
                                        <img style="border-radius: 50%;" src="https://scontent.xx.fbcdn.net/v/t1.0-1/cp0/p48x48/55738269_2813143815370024_4176931368388788224_n.jpg?_nc_cat=106&amp;_nc_sid=dbb9e7&amp;_nc_ohc=r1_aiKlQr2oAX9slP3O&amp;_nc_ad=z-m&amp;_nc_cid=0&amp;_nc_ht=scontent.xx&amp;oh=0b4355eaa6036f3654a35daeac226554&amp;oe=5F0C85E2">
                                    </div>
                                    <div style="float:left; width:75%;">
                                        <span>
                                            <span>Nước hoa Charme Bình Dương</span>
                                            <span> checked in at </span>
                                            <span>CALLA Cosmetics</span>
                                            <span>: "1. Nước hoa Charme..."</span>
                                        </span>
                                        <div>
                                            <div style="float:left;">
                                                <img src="https://www.facebook.com/rsrc.php/v3/yT/r/-IWvKsOGqat.png">
                                            </div>
                                            <div>
                                                <span>
                                                    <i>
                                                        <span class="timestampContent">10m - ${v.currentDate}</span>
                                                    </i>
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                    <div style="font-size:12px;">
                                         <div><a href="#" alt="Remove this notification">X</a></div>
                                         <div><a href="#" alt="Mark as read">...</a></div>
                                    </div>
                                </div>
                            </a>
                        </li>`;

                });

                $("#notificationBody").html(tr);
                $("#notification_count").html(result.length);
            },
            error: (error) => {
                console.log(error);
            }
        });
    }
});