import { Inbox, Search, Settings, Calendar, SquarePlus, PanelsTopLeft } from "lucide-react";
import {
    Sidebar,
    SidebarContent,
    SidebarGroup, SidebarGroupContent,
    SidebarGroupLabel, SidebarMenu,
    SidebarMenuButton, SidebarMenuItem
} from "@/components/ui/sidebar.tsx";

const items = [
    {
        title: "Create",
        url: "/create-reference",
        icon: SquarePlus,
    },
    {
        title: "Library",
        url: "/library",
        icon: PanelsTopLeft,
    },
    {
        title: "Calendar",
        url: "#",
        icon: Calendar,
    },
    {
        title: "Search",
        url: "#",
        icon: Search,
    },
    {
        title: "Settings",
        url: "#",
        icon: Settings,
    },
]
export function WorkspaceToolBar() {
    return (
        <Sidebar className={"flex-1 mt-16"}>
            <SidebarContent>
                <SidebarGroup>
                    <SidebarGroupLabel>Application</SidebarGroupLabel>
                    <SidebarGroupContent>
                        <SidebarMenu>
                            {items.map((item) => (
                                <SidebarMenuItem key={item.title}>
                                    <SidebarMenuButton asChild>
                                        <a href={item.url}>
                                            <item.icon />
                                            <span>{item.title}</span>
                                        </a>
                                    </SidebarMenuButton>
                                </SidebarMenuItem>
                            ))}
                        </SidebarMenu>
                    </SidebarGroupContent>
                </SidebarGroup>
            </SidebarContent>
        </Sidebar>
        
        // <nav className="flex-1">
        //     <ul className="space-y-2">
        //         <li>
        //             <Link
        //                 to={"/create-reference"}
        //                 className="flex items-center gap-2 text-foreground font-semibold"
        //             >
        //                 <SquarePlus />
        //                 <Label className={"w-full text-left"}> Create </Label>
        //             </Link>
        //         </li>
        //         <li>
        //             <Link
        //                 to={"/library"}
        //                 className="flex items-center gap-2 text-foreground font-semibold"
        //             >
        //                 <SquarePlus />
        //                 <Label className={"w-full text-left"}>
        //                     {" "}
        //                     View References{" "}
        //                 </Label>
        //             </Link>
        //         </li>
        //         <li>
        //             <Link
        //                 to={"/create-reference"}
        //                 className="flex items-center gap-2 text-foreground font-semibold"
        //             >
        //                 <SquarePlus />
        //                 <Label className={"w-full text-left"}> Groups </Label>
        //             </Link>
        //         </li>
        //         <li>
        //             <Link
        //                 to={"/create-reference"}
        //                 className="flex items-center gap-2 text-foreground font-semibold"
        //             >
        //                 <SquarePlus />
        //                 <Label className={"w-full text-left"}> Tags </Label>
        //             </Link>
        //         </li>
        //         <li>
        //             <Link
        //                 to={"/create-reference"}
        //                 className="flex items-center gap-2 text-foreground font-semibold"
        //             >
        //                 <SquarePlus />
        //                 <Label className={"w-full text-left"}> Settings </Label>
        //             </Link>
        //         </li>
        //     </ul>
        // </nav>
    );
}
