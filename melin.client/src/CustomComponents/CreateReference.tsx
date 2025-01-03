﻿"use client";

import { ReferenceTypeSelector } from "@/CreateRefComponents/ReferenceTypeSelector.tsx";
import { useState } from "react";
import { BaseReferenceCreator } from "@/CreateRefComponents/BaseReferenceCreator.tsx";
import {
    artworkSchema,
    bookSchema,
    legalCaseSchema,
    legislationSchema,
    patentSchema,
    presentationSchema,
    primarySourceSchema,
    recordingSchema,
    reportSchema,
    softwareSchema,
    websiteSchema,
} from "@/ReferenceCreationPages/BaseReferenceSchema.ts";
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
export function CreateReference() {
    const [refType, setRefType] = useState("artwork"); // default is book

    function handleState(newRefType: string) {
        setRefType(newRefType);
    }

    return (
        <>
            <div className={"grid grid-cols-3 gap-4"}>
                <div className={"col-span-2"}>
                    {refType === "book" && (
                        <BaseReferenceCreator
                            refSchema={bookSchema}
                            schemaName="book"
                        />
                    )}
                    {refType === "artwork" && (
                        <BaseReferenceCreator
                            refSchema={artworkSchema}
                            schemaName="artwork"
                        />
                    )}
                    {refType === "case" && (
                        <BaseReferenceCreator
                            refSchema={legalCaseSchema}
                            schemaName="case"
                        />
                    )}
                    {refType === "patent" && (
                        <BaseReferenceCreator
                            refSchema={patentSchema}
                            schemaName="patent"
                        />
                    )}
                    {refType === "legislation" && (
                        <BaseReferenceCreator
                            refSchema={legislationSchema}
                            schemaName="legislation"
                        />
                    )}
                    {refType === "website" && (
                        <BaseReferenceCreator
                            refSchema={websiteSchema}
                            schemaName="website"
                        />
                    )}
                    {refType === "report" && (
                        <BaseReferenceCreator
                            refSchema={reportSchema}
                            schemaName="report"
                        />
                    )}
                    {refType === "presentation" && (
                        <BaseReferenceCreator
                            refSchema={presentationSchema}
                            schemaName="presentation"
                        />
                    )}
                    {refType === "primary-source" && (
                        <BaseReferenceCreator
                            refSchema={primarySourceSchema}
                            schemaName="primary-source"
                        />
                    )}
                    {refType === "audio-recording" && (
                        <BaseReferenceCreator
                            refSchema={recordingSchema}
                            schemaName="audio-recording"
                        />
                    )}
                    {refType === "software" && (
                        <BaseReferenceCreator
                            refSchema={softwareSchema}
                            schemaName="software"
                        />
                    )}
                </div>
                <div className={"space-y-2"}>
                    <Card>
                        <CardHeader>
                            <CardTitle>Reference Type</CardTitle>
                        </CardHeader>
                        <CardContent>
                            <ReferenceTypeSelector
                                refType={refType}
                                handleState={handleState}
                            />
                        </CardContent>
                    </Card>
                </div>
            </div>
        </>
    );
}
