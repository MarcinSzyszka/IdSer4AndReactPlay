import { UserManager } from 'oidc-client'

export class OidcUserManagerSingletonFactory {
    static instance;

    static GetInstance = function () {
        if (!this.instance) {
            this.instance = new UserManager({
                authority: "https://localhost:44375",
                client_id: "reactappjs",
                redirect_uri: "https://localhost:44381/callback",
                silent_redirect_uri: "https://localhost:44381/silentSignInCallback.html",
                response_type: "code",
                response_mode: 'query',
                scope: "openid profile api1",
                post_logout_redirect_uri: "https://localhost:44381/"
            });
        }

        return this.instance;
    }
}