import client, { authClient } from "../../../lib/axios";
import type { AuthDetails, LoginInput } from "../../../types/User";

export const loginUser = async (loginInput : LoginInput) => {
    const res = await authClient.post<AuthDetails>('/Auth/login', loginInput, {
        headers: {
            'Content-Type': 'application/json'
        },
        withCredentials: true
    })
    
    return res.data
}


export const logoutUser = async () => {
    const res = await client.post<AuthDetails>('/Auth/logout', {})
    
    return res.data
}