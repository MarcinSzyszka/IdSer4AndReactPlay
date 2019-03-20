import React, { Component } from 'react';
import { OidcUserManagerSingletonFactory } from './oidcUserManager'

export class SignInCallback extends Component {
    constructor(props) {
        super(props);
        this.state = {};
    }

    componentDidMount() {
        OidcUserManagerSingletonFactory.GetInstance().signinRedirectCallback().then(function (user) {
            window.location = user.state;
        }).catch(function (e) {
            if (e.error === 'login_required') {
                console.warn('Login required!');
            }
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