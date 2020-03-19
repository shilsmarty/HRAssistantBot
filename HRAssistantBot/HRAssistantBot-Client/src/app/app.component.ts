import { Component, ElementRef, OnInit, ViewChild } from "@angular/core";

/**
 * Declares the WebChat property on the window object.
 */
declare global {
    interface Window {
        WebChat: any;
    }
}

window.WebChat = window.WebChat || {};

@Component({
    selector: "app-root",
    templateUrl: "./app.component.html",
    styleUrls: ["./app.component.scss"]
})
export class AppComponent implements OnInit {
    @ViewChild("botWindow") botWindowElement: ElementRef;

    public ngOnInit(): void {
        const directLine = window.WebChat.createDirectLine({
            secret: "FjnQR-P6L7s.-y6co7qVICagfkRZzR2CZR6BQ5fPQMO5obtAfpk6IPE",
            webSocket: false
        });

        window.WebChat.renderWebChat(
            {
                directLine: directLine,
                webSpeechPonyfillFactory: window.WebChat.createBrowserWebSpeechPonyfillFactory(),
                userID: "shilsr",
                botAvatarInitials: 'Assist',
                userAvatarInitials: 'Tom'
            },
            this.botWindowElement.nativeElement
        );

        directLine
            .postActivity({
                from: { id: "shilsr", name: "shiladitya srivastava" },
                name: "requestWelcomeDialog",
                type: "event",
                value: "FjnQR-P6L7s.dAA.cQBGADIAQgBkAFYAawBwADkARgBEAGkAdABHAHYAQgBWAGsAdQA1AHMALQBnAA.zUaaNyjl1AE.A6UrLhk_18U.dFfUGs-35OiF9iLXpKSRTQrR506mAnmkJ4ll8hTw_RQ"
            })
            .subscribe(
                id => console.log(`Posted activity, assigned ID ${id}`),
                error => console.log(`Error posting activity ${error}`)
            );
    }
}
