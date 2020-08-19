
/** ------------------------------------------------------
 * THIS FILE WAS AUTOMATICALLY GENERATED (DO NOT MODIFY)
 * -------------------------------------------------------
 */

/* tslint:disable */
/* eslint-disable */
export interface CreateUserInput {
    name: string;
    email: string;
    password: string;
}

export interface User {
    id: string;
    tag: number;
    name: string;
    email: string;
    status: string;
    avatarUrl: string;
    createdAt: string;
}

export interface IMutation {
    createAccount(data?: CreateUserInput): User | Promise<User>;
}

export interface IQuery {
    me(): User | Promise<User>;
    user(id: string): User | Promise<User>;
    users(): User[] | Promise<User[]>;
}
