"use client";

import { BaseReferenceCreator } from "@/routes/CustomComponents/CreateRefComponents/BaseReferenceCreator.tsx";
import { ReferenceTypeSelector } from "@/routes/CustomComponents/CreateRefComponents/ReferenceTypeSelector.tsx";
import { useState } from "react";
import { DndContext } from "@dnd-kit/core";
import { Draggable } from "@/routes/CustomComponents/DragNDrop/Draggable.tsx";
import { Droppable } from "@/routes/CustomComponents/DragNDrop/Droppable.tsx";
export function CreateReference() {
    const [refType, setRefType] = useState("book"); // default is book
    const [isDropped, setIsDropped] = useState(false);
    const draggableMarkup = <Draggable>Drag me</Draggable>;
    function handleState(newRefType: string) {
        setRefType(newRefType);
        console.log(refType);
    }
    return (
        <>
            <div className={"grid grid-cols-2 gap-8"}>
                <DndContext onDragEnd={handleDragEnd}>
                    {!isDropped ? draggableMarkup : null}
                    <Droppable>
                        {isDropped ? draggableMarkup : "Drop here"}
                    </Droppable>
                    <ReferenceTypeSelector
                        refType={refType}
                        handleState={handleState}
                    />
                    <BaseReferenceCreator />
                </DndContext>
            </div>
        </>
    );
    function handleDragEnd(event: any) {
        if (event.over && event.over.id === "droppable") {
            setIsDropped(true);
        }
    }
}
