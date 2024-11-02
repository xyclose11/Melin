"use client";

import { useEffect, useState } from "react";
import { instance } from "@/utils/axiosInstance.ts";
import { LibrarySideBar } from "@/routes/LibraryViews/LibrarySideBar.tsx";
import { DraggableGroup } from "@/routes/GroupComponents/DraggableGroup.tsx";
import { Library } from "@/routes/Library.tsx";
import { ReferenceSelectionProvider } from "@/routes/Context/ReferencesSelectedContext.tsx";
import { ToastAction } from "@/components/ui/toast.tsx";
import { useToast } from "@/hooks/use-toast.ts";

export enum CREATOR_TYPES {
    Author = "Author",
}

type GroupType = {
    name: string;
    references: [];
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

    return (
        <div className={"flex gap-3"}>
            <ReferenceSelectionProvider>
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
            </ReferenceSelectionProvider>
        </div>
    );
}
