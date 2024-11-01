import { Outlet } from "react-router-dom";
import { LibrarySideBar } from "@/routes/LibraryViews/LibrarySideBar.tsx";
import { DraggableGroup } from "@/routes/GroupComponents/DraggableGroup.tsx";
import { instance } from "@/utils/axiosInstance.ts";
import React, { ReactNode, useEffect, useState } from "react";
// import { DndContext } from "@dnd-kit/core";
// import { useState } from "react";
// import { LibrarySideBar } from "@/routes/LibraryViews/LibrarySideBar.tsx";
// import { DroppableWorkspace } from "@/routes/LibraryViews/DragNDrop/DroppableWorkspace.tsx";

export function Workspace() {
    // const [isDropped, setIsDropped] = useState(false);
    // const libSideBar = <LibrarySideBar>Drag me</LibrarySideBar>;

    const [userGroups, setUserGroups] = useState<ReactNode[]>([]);

    const getGroups = async () => {
        try {
            const res = await instance.get("get-owned-groups", {
                withCredentials: true,
            });

            if (res.status === 200) {
                console.log(res);
                // see if user has groups
                if (res.data.length <= 0) {
                    console.log("NO GROUPS");
                }

                setUserGroups(res.data);
            } else {
                // DISPLAY ERROR
            }
        } catch (e) {
            console.log(e);
            // DISPLAY ERROR
        }
    };

    useEffect(() => {
        getGroups();
    }, []);

    return (
        <div className="flex-grow ml-12 mr-36 justify-center">
            {/*<DndContext onDragEnd={handleDragEnd}>*/}
            {/*    {!isDropped ? libSideBar : null}*/}
            {/*    <DroppableWorkspace>*/}
            {/*        {isDropped ? libSideBar : "Drop Here"}*/}
            {/*    </DroppableWorkspace>*/}
            {/*</DndContext>*/}
            <div className="flex gap-3">
                <LibrarySideBar>
                    <DraggableGroup>
                        {userGroups.map((g) => (
                            <div>{g}</div>
                        ))}
                    </DraggableGroup>
                </LibrarySideBar>
                <Outlet />
            </div>
        </div>
    );

    // function handleDragEnd(event: any) {
    //     if (event.over && event.over.id === "droppable") {
    //         setIsDropped(true);
    //     }
    // }
}
