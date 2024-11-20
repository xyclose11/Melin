"use client";

import { useEffect, useState } from "react";
import { instance } from "@/utils/axiosInstance.ts";
import { LibrarySideBar } from "@/routes/LibraryViews/LibrarySideBar.tsx";
import { DraggableGroup } from "@/routes/GroupComponents/DraggableGroup.tsx";
import { Library } from "@/routes/Library.tsx";
import { ReferenceSelectionProvider } from "@/routes/Context/ReferencesSelectedContext.tsx";
import { ToastAction } from "@/components/ui/toast.tsx";
import { useToast } from "@/hooks/use-toast.ts";
import { DndContext, DragEndEvent } from "@dnd-kit/core";
import { GroupSelectedProvider } from "@/routes/Context/SelectedGroupContext.tsx";

export enum CREATOR_TYPES {
    Author = "Author",
}

export type GroupType = {
    id: number;
    name: string;
    references: [];
    groups: [];
    isRoot: boolean;
};

type Creator = {
    id: number;
    type: CREATOR_TYPES;
    firstName: string;
    lastName: string;
};

export type Reference = {
    id: number;
    type: string;
    title: string;
    creators: Creator[];
    language: string;
};

export function LibraryPage() {
    const [userGroups, setUserGroups] = useState<GroupType[]>([]);
    const { toast } = useToast();

    const getGroups = async () => {
        try {
            const res = await instance.get("get-owned-groups", {
                withCredentials: true,
            });

            if (res.status === 200) {
                setUserGroups(res.data);
            } else {
                toast({
                    variant: "destructive",
                    title: "Unable to Populate Groups",
                    action: (
                        <ToastAction altText={"Try Again"}>
                            Try Again
                        </ToastAction>
                    ),
                });
            }
        } catch (e) {
            toast({
                variant: "destructive",
                title: "Unable to Populate Groups",
                action: (
                    <ToastAction altText={"Try Again"}>Try Again</ToastAction>
                ),
            });
            console.log(e);
        }
    };

    useEffect(() => {
        getGroups();
    }, []);

    function handleDragEnd(event: DragEndEvent) {
        // add drag group to drop group
        const drag = event.active.id.valueOf();
        const parent = event.over?.id.valueOf();

        if (drag !== parent && drag !== undefined && parent !== undefined) {
            const data: GroupDTO = {
                parent: parent
                    .toString()
                    .substring(0, parent.toString().length - 5),
                child: drag.toString().substring(0, drag.toString().length - 5),
            };

            addGroupToGroup(data).then((r) => console.log(r));
        }
    }

    type GroupDTO = {
        parent: string;
        child: string;
    };

    const addGroupToGroup = async (data: GroupDTO) => {
        try {
            const res = await instance.post("add-group-to-group", data, {
                withCredentials: true,
            });

            if (res.status === 200) {
                // TODO ADD ALERTS
                console.log(res);
            } else {
                console.log(res);
            }
        } catch (event) {
            console.error(event);
        }
    };

    // TODO MAKE REMOVING REFERENCES FROM GROUP STATEFUL

    return (
        <div className={"flex gap-3"}>
            <ReferenceSelectionProvider>
                <GroupSelectedProvider>
                    <LibrarySideBar>
                        <DndContext onDragEnd={handleDragEnd}>
                            {userGroups
                                .filter((g) => g.isRoot)
                                .map((g: GroupType) => (
                                    <DraggableGroup
                                        key={g.id}
                                        groupName={g.name}
                                        groups={g.groups}
                                        references={g.references}
                                    ></DraggableGroup>
                                ))}
                        </DndContext>
                    </LibrarySideBar>{" "}
                    <Library />
                </GroupSelectedProvider>
            </ReferenceSelectionProvider>
        </div>
    );
}
