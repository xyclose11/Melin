import { Search, Settings, SquarePlus, PanelsTopLeft, Boxes, Tag } from "lucide-react";
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
        title: "Search",
        url: "#",
        icon: Search,
    },
    {
        title: "Groups",
        url: "/groups",
        icon: Boxes,
    },
    {
        title: "Tags",
        url: "#",
        icon: Tag,
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
                    <SidebarGroupLabel>Melin</SidebarGroupLabel>
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
    );
}
