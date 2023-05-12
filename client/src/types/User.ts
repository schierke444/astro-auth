export interface AuthDetails {
    id: string,
    username: string,
    accessToken: string
}


export interface LoginInput {
    username: string,
    password: string
}