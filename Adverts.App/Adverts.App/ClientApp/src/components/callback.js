import React, { Component } from 'react';
import { UserManager, OidcClient } from 'oidc-client'

export class Callback extends Component {
    constructor(props) {
        super(props);

        // new UserManager({ response_mode: "query" }).signinRedirectCallback().then(function (aaa) {
        //     debugger;
        //     window.location = "index.html";
        // }).catch(function (e) {
        //     debugger;
        //     console.error(e);
        // });

        // new OidcClient().processSigninResponse().then(function(response) {
        //     console.log("signin response success", response);
        // }).catch(function(err) {
        //     console.error(err);
        // });
    }
    componentDidMount() {

        new UserManager({ response_mode: "query" }).signinRedirectCallback().then(function (aaa) {
            debugger;
            window.location = "index.html";
        }).catch(function (e) {
            debugger;
            console.error(e);
        });

        new OidcClient().processSigninResponse().then(function(response) {
            console.log("signin response success", response);
        }).catch(function(err) {
            console.error(err);
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