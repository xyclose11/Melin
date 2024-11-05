import { Badge } from "@/components/ui/badge.tsx";
import {
    ContextMenu,
    ContextMenuContent,
    ContextMenuItem,
    ContextMenuSub,
    ContextMenuSubContent,
    ContextMenuSubTrigger,
    ContextMenuTrigger,
} from "@/components/ui/context-menu.tsx";

export function TagTableDisplay({
    name,
}: {
    name: string;
}) {
    return (
        <>
            <ContextMenu>
                <ContextMenuTrigger>
                    <Badge variant="secondary">{name}</Badge>
                </ContextMenuTrigger>
                <ContextMenuContent>
                    <ContextMenuItem>
                        <ContextMenuSub>
                            <ContextMenuSubTrigger>Edit</ContextMenuSubTrigger>
                            <ContextMenuSubContent>
                                {/* <CreateGroupForm /> */}
                            </ContextMenuSubContent>
                        </ContextMenuSub>
                    </ContextMenuItem>
                    <ContextMenuItem>Remove From Reference</ContextMenuItem>
                </ContextMenuContent>
            </ContextMenu>
        </>
    );
}
