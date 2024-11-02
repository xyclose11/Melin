"use client";

import { ReactNode, useEffect, useState } from "react";
import { instance } from "@/utils/axiosInstance.ts";
import { LibrarySideBar } from "@/routes/LibraryViews/LibrarySideBar.tsx";
import { DraggableGroup } from "@/routes/GroupComponents/DraggableGroup.tsx";
import { Library } from "@/routes/Library.tsx";

export enum CREATOR_TYPES {
    Author = "Author",
}

type GroupType = {
    name: string;
    nodes: [];
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
        <div className={"flex gap-3"}>
            <LibrarySideBar>
                {userGroups.map((g: GroupType) => (
                    <DraggableGroup
                        key={g.name}
                        groupName={g.name}
                        groupNodes={g.references}
                    />
                ))}
            </LibrarySideBar>{" "}
            <Library />
        </div>
    );
}
