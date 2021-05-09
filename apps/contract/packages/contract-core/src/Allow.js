import { AllowException } from "./exceptions";
import { createHttpClient } from "./http";

/**
 * @description The class for accessing Allow Api routes
 */
export class Allow {
    /**
     * @type {import("axios").AxiosInstance}
     */
    #client

    /** 
     * @param {string} baseUrl - The Allow Api base url
     */
    constructor(baseUrl) {
        this.#client = createHttpClient(baseUrl)
    }

    /** 
     * @param {string} email
     * @param {string} password
     * @returns {string} accessToken
     */
    async login(email, password) {
        try {
            const response = await this.#client.post('/Account/Login', {
                email,
                password
            })
            if (response.data.accessToken) {
                return response.data.accessToken
            }
        } catch {}

        throw new AllowException({
            login: [
                "Email or password are incorrect"
            ]
        })
    }
}