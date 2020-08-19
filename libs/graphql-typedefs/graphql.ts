/** ------------------------------------------------------
 * THIS FILE WAS AUTOMATICALLY GENERATED (DO NOT MODIFY)
 * -------------------------------------------------------
 */

/* tslint:disable */
/* eslint-disable */
export interface IQuery {
    me(): User | Promise<User>;
}

export interface IMutation {
    login(email: string): User | Promise<User>;
}

export interface User {
    email: string;
}
