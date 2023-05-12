import QueryProvider from "./QueryProvider"

export const QueryProviderHoc = (Comp: React.FC) => {
    const newComp = () => {
        return (
            <QueryProvider>
                <Comp />
            </QueryProvider>
        )
    }

    return newComp
}