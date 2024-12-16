import { createRootRoute } from "@tanstack/react-router";
import Root from "@/root.tsx";
import { CookiesProvider } from "react-cookie";

export const Route = createRootRoute({
    component: () => (
        <>
            <CookiesProvider>
                <Root children />
            </CookiesProvider>
        </>
    ),
});
