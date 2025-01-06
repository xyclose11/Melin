"use client";

import { useEffect, useState } from "react";
import { instance } from "@/utils/axiosInstance.ts";
import { LibrarySideBar } from "@/LibraryViews/LibrarySideBar.tsx";
import { DraggableGroup } from "@/GroupComponents/DraggableGroup.tsx";
import { Library, Reference } from "@/Library.tsx";
import { ReferenceSelectionProvider } from "@/Context/ReferencesSelectedContext.tsx";
import { ToastAction } from "@/components/ui/toast.tsx";
import { useToast } from "@/hooks/use-toast.ts";
import { DndContext, DragEndEvent } from "@dnd-kit/core";
import { GroupSelectedProvider } from "@/Context/SelectedGroupContext.tsx";
import { Group } from "@/LibraryViews/GroupLibrary.tsx";
import {
    ResizableHandle,
    ResizablePanel,
    ResizablePanelGroup,
} from "@/components/ui/resizable.tsx";

export enum CREATOR_TYPES {
    Author = "Author",
}

export type GroupType = {
    id: number;
    name: string;
    references: [];
    childGroups: GroupType[];
    isRoot: boolean;
};

export function LibraryPage({ initialData }: { initialData: Reference[] }) {
    const [userGroups, setUserGroups] = useState<GroupType[]>([]);
    const { toast } = useToast();

    const handleAddToUserGroup = (newGroup: Group) => {
        // convert Group to GroupType
        const updatedGroup: GroupType = {
            isRoot: true,
            references: [],
            name: newGroup.name,
            childGroups: [],
            id: newGroup.id,
        };
        setUserGroups([...userGroups, updatedGroup]);
    };

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
                // remove child group from UserGroup state
                const childGroup = userGroups.find(
                    (g) => g.name === data.child,
                );
                if (!childGroup) {
                    console.error("Child group not found in userGroups");
                    return;
                }
                const updatedGroups = userGroups.map((group) => {
                    if (
                        group.childGroups.some(
                            (child) => child.name === data.child,
                        )
                    ) {
                        return {
                            ...group,
                            childGroups: group.childGroups.filter(
                                (child) => child.name !== data.child,
                            ),
                        };
                    }
                    if (group.name === data.parent) {
                        return {
                            ...group,
                            childGroups: [...group.childGroups, childGroup],
                        };
                    }
                    return group;
                });

                setUserGroups(updatedGroups);
            } else {
                console.log(res);
            }
        } catch (event) {
            console.error(event);
        }
    };

    return (
        <div className={"mr-10 mt-16 mb-12 ml-4 justify-center flex gap-4"}>
            <ReferenceSelectionProvider>
                <GroupSelectedProvider>
                    <ResizablePanelGroup
                        autoSaveId="persistent-group-sidebar"
                        direction="horizontal"
                    >
                        <ResizablePanel
                            defaultSize={15}
                            minSize={10}
                            maxSize={30}
                        >
                            <LibrarySideBar
                                handleAddToUserGroup={handleAddToUserGroup}
                            >
                                <DndContext onDragEnd={handleDragEnd}>
                                    {userGroups
                                        .filter((g) => g.isRoot)
                                        .map((g: GroupType) => (
                                            <DraggableGroup
                                                key={g.id}
                                                groupName={g.name}
                                                childGroups={g.childGroups}
                                                references={g.references}
                                            ></DraggableGroup>
                                        ))}
                                </DndContext>
                            </LibrarySideBar>
                        </ResizablePanel>
                        <ResizableHandle withHandle />
                        <ResizablePanel>
                            <Library initialData={initialData} />
                        </ResizablePanel>
                    </ResizablePanelGroup>
                </GroupSelectedProvider>
            </ReferenceSelectionProvider>
        </div>
    );
}
