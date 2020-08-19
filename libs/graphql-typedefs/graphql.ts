
/** ------------------------------------------------------
 * THIS FILE WAS AUTOMATICALLY GENERATED (DO NOT MODIFY)
 * -------------------------------------------------------
 */

/* tslint:disable */
/* eslint-disable */
export interface CreateUserInput {
    name: string;
    email: string;
}

export interface User {
    id: string;
    name: string;
    email: string;
    createdAt: string;
}

export interface IMutation {
    createAccount(data?: CreateUserInput): User | Promise<User>;
}

export interface IQuery {
    users(): User[] | Promise<User[]>;
    user(id: string): User | Promise<User>;
}
