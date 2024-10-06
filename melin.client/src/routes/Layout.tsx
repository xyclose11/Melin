﻿import { Link } from "react-router-dom";

import { CircleUser, Search, SquareLibrary } from "lucide-react";

import { Button } from "@/components/ui/button";
import {
    DropdownMenu,
    DropdownMenuContent,
    DropdownMenuItem,
    DropdownMenuLabel,
    DropdownMenuSeparator,
    DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu";
import { Input } from "@/components/ui/input";
import LogoutLink from "@/routes/CustomComponents/LogoutLink.tsx";
import UserSettingsLink from "@/routes/CustomComponents/UserSettingsLink.tsx";
import { useAuth } from "@/utils/AuthProvider.tsx";

export function NavBar() {
    const { isAuthenticated } = useAuth(); // Assuming you have an AuthContext

    return (
        <header className="fixed top-0 p-4 flex w-screen justify-center h-16 items-center gap-4 border-b bg-background">
            <nav className="hidden flex-col gap-6 text-lg font-medium md:flex md:flex-row md:items-center md:gap-5 md:text-sm lg:gap-6">
                <Link
                    to={"#"}
                    className="flex items-center gap-2 text-lg font-semibold md:text-base"
                >
                    <SquareLibrary className="h-6 w-6" />
                    <span className="sr-only">Melin</span>
                </Link>
                <Link
                    to={"/"}
                    className="text-foreground transition-colors hover:text-foreground"
                >
                    Home
                </Link>
                <Link
                    to={"/dashboard"}
                    className="text-foreground transition-colors hover:text-foreground"
                >
                    Dashboard
                </Link>
                <Link
                    to={"/groups"}
                    className="text-foreground transition-colors hover:text-foreground"
                >
                    Groups
                </Link>
                <Link
                    to={"/library"}
                    className="text-foreground transition-colors hover:text-foreground"
                >
                    Library
                </Link>
            </nav>
            <div className="flex w-full items-center gap-4 md:ml-auto md:gap-2 lg:gap-4">
                <form className="ml-auto flex-1 sm:flex-initial">
                    <div className="relative">
                        <Search className="absolute left-2.5 top-2.5 h-4 w-4 text-muted-foreground" />
                        <Input
                            type="search"
                            placeholder="Search..."
                            className="pl-8 sm:w-[300px] md:w-[200px] lg:w-[300px]"
                        />
                    </div>
                </form>
                <DropdownMenu>
                    <DropdownMenuTrigger asChild>
                        <Button
                            variant="secondary"
                            size="icon"
                            className="rounded-full"
                        >
                            <CircleUser className="h-5 w-5" />
                            <span className="sr-only">Toggle user menu</span>
                        </Button>
                    </DropdownMenuTrigger>

                    <DropdownMenuContent align="end">
                        {!isAuthenticated ? (
                            <DropdownMenuItem>
                                <Link
                                    to="/login"
                                    className="text-muted-foreground transition-colors hover:text-foreground"
                                >
                                    Login
                                </Link>
                            </DropdownMenuItem>
                        ) : (
                            <>
                                <DropdownMenuLabel>
                                    My Account
                                </DropdownMenuLabel>
                                <DropdownMenuSeparator />
                                <DropdownMenuItem>
                                    <UserSettingsLink />
                                </DropdownMenuItem>
                                <DropdownMenuSeparator />
                                <DropdownMenuItem>
                                    <LogoutLink />
                                </DropdownMenuItem>
                            </>
                        )}
                    </DropdownMenuContent>
                </DropdownMenu>
            </div>
        </header>
    );
}
