
/** ------------------------------------------------------
 * THIS FILE WAS AUTOMATICALLY GENERATED (DO NOT MODIFY)
 * -------------------------------------------------------
 */

/* tslint:disable */
/* eslint-disable */
export interface CreateAccountInput {
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

export interface CreateAccountResponse {
    token: string;
    user: User;
}

export interface LoginResponse {
    token: string;
}

export interface IMutation {
    createAccount(data?: CreateAccountInput): CreateAccountResponse | Promise<CreateAccountResponse>;
    updateAccount(data?: UpdateAccountInput): User | Promise<User>;
    uploadAvatar(file?: Upload): User | Promise<User>;
    login(data?: LoginInput): LoginResponse | Promise<LoginResponse>;
}

export interface IQuery {
    me(): User | Promise<User>;
    userById(id: string): User | Promise<User>;
    userByNameTag(nameTag: string): User | Promise<User>;
    users(): User[] | Promise<User[]>;
}

export type Upload = any;
