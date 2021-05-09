export class AllowException extends Error {
    /**
     * @type {Record<string, Array<string>>}
     */
    errors
    /**
     * @param {Record<string, Array<string>>} errors
     */
    constructor(errors) {
        this.errors = errors
        super("One or more errors ocurred")
    }
}