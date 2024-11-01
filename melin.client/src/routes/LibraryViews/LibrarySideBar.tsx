import {
    Card,
    CardContent,
    CardFooter,
    CardHeader,
    CardTitle,
} from "@/components/ui/card.tsx";
import { useDraggable } from "@dnd-kit/core";
import {
    ContextMenu,
    ContextMenuContent,
    ContextMenuItem,
    ContextMenuSub,
    ContextMenuSubContent,
    ContextMenuSubTrigger,
    ContextMenuTrigger,
} from "@/components/ui/context-menu.tsx";
import {
    Popover,
    PopoverContent,
    PopoverTrigger,
} from "@/components/ui/popover.tsx";
import { useState } from "react";
import { PopoverAnchor } from "@radix-ui/react-popover";
import { CreateGroupForm } from "@/routes/GroupComponents/CreateGroupForm.tsx";

export function LibrarySideBar(props: any) {
    const { attributes, listeners, setNodeRef, transform } = useDraggable({
        id: "draggable",
    });
    const style = transform
        ? {
              transform: `translate3d(${transform.x}px, ${transform.y}px, 0)`,
          }
        : undefined;

    return (
        <>
            <Card ref={setNodeRef} style={style} {...listeners} {...attributes}>
                <CardHeader>
                    <CardTitle>Groups</CardTitle>
                </CardHeader>
                <ContextMenu>
                    <ContextMenuTrigger>
                        <CardContent>{props.children}</CardContent>
                    </ContextMenuTrigger>
                    <ContextMenuContent>
                        <ContextMenuSub>
                            <ContextMenuSubTrigger>
                                Create Group
                            </ContextMenuSubTrigger>
                            <ContextMenuSubContent>
                                <CreateGroupForm />
                            </ContextMenuSubContent>
                        </ContextMenuSub>
                    </ContextMenuContent>
                </ContextMenu>
                <CardFooter></CardFooter>
            </Card>
        </>
    );
}
