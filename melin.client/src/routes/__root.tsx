import { createRootRouteWithContext } from "@tanstack/react-router";
import Root from "@/root.tsx";
import { CookiesProvider } from "react-cookie";
import { TanStackRouterDevtools } from "@tanstack/router-devtools";
import { ReactQueryDevtools } from "@tanstack/react-query-devtools";
import { Footer } from "@/Footer.tsx";
import { QueryClient } from "@tanstack/react-query";
import { AuthContextType } from "@/utils/AuthProvider.tsx";

export const Route = createRootRouteWithContext<{
    auth: AuthContextType;
    queryClient: QueryClient;
}>()({
    component: () => (
        <div className="overflow-hidden">
            <CookiesProvider>
                <Root children />
            </CookiesProvider>
            <Footer />

            <TanStackRouterDevtools />
            <ReactQueryDevtools />
        </div>
    ),
});
