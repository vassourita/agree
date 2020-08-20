
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

export interface UpdateAccountInput {
    name?: string;
    status?: string;
}

export interface LoginInput {
    email: string;
    password: string;
}

export interface User {
    id: string;
    tag: number;
    name: string;
    email: string;
    status?: string;
    avatarUrl?: string;
    createdAt: string;
}

export interface LoginResponse {
    token: string;
}

export interface CreateAccountResponse {
    token: string;
    user: User;
}

export interface IMutation {
    createAccount(data?: CreateUserInput): CreateAccountResponse | Promise<CreateAccountResponse>;
    updateAccount(data?: UpdateAccountInput): User | Promise<User>;
    login(data?: LoginInput): LoginResponse | Promise<LoginResponse>;
}

export interface IQuery {
    me(): User | Promise<User>;
    userById(id: string): User | Promise<User>;
    userByNameTag(nameTag: string): User | Promise<User>;
    users(): User[] | Promise<User[]>;
}
