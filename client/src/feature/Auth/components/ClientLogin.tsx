import { useEffect } from "react"
import QueryProvider from "../../../provider/QueryProvider"
import LoginUser from "./LoginUser"
import client from "../../../lib/axios"

const ClientLogin = () => {


    return (
        <QueryProvider>
            <div className="max-w-md mx-auto mt-[50px]">
                <LoginUser /> 
            </div> 
        </QueryProvider>
    )
}

export default ClientLogin