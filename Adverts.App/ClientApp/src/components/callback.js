import React, { Component } from 'react';
import { UserManager } from 'oidc-client'

export class Callback extends Component {
    constructor(props) {
        super(props);
        this.state = {};
    }

    componentDidMount() {
        new UserManager({ response_mode: "query" }).signinRedirectCallback().then(function (user) {
            window.location = user.state;
        }).catch(function (e) {
            console.error(e);
        });

        new UserManager().signinSilentCallback().then(function (aaa) {
            window.location = "index.html";
        }).catch(function (e) {
            console.error(e);
        });
    }


    render() {
        return (
            <div>
                Callback component
            </div>
        );
    }
}