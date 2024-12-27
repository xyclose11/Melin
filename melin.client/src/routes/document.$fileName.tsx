import { createFileRoute } from "@tanstack/react-router";
// import Connector from "@/DocumentEditing/documentActions.ts";
import React, { useEffect } from "react";
import { fetchDocument } from "@/api/fetchDocument.ts";
import { cva } from "class-variance-authority";
import { cn, withProps } from "@udecode/cn";
import {
    PlateContent,
    useEditorContainerRef,
    useEditorRef,
} from "@udecode/plate-common/react";
import type { PlateContentProps } from "@udecode/plate-common/react";
import type { VariantProps } from "class-variance-authority";
import { Plate, usePlateEditor } from "@udecode/plate-common/react";
import { HeadingPlugin } from "@udecode/plate-heading/react";
import {
    ItalicPlugin,
    UnderlinePlugin,
} from "@udecode/plate-basic-marks/react";
import { PlateElement, PlateLeaf } from "@udecode/plate-common/react";

export const Route = createFileRoute("/document/$fileName")({
    loader: async ({ params }) => {
        return fetchDocument(params.fileName);
    },
    component: DocumentComponent,
});

function DocumentComponent() {
    // const { newMessage, events } = Connector();
    // const [message, setMessage] = useState("initial value");
    const document = Route.useLoaderData();
    const localValue =
        typeof window !== "undefined" && localStorage.getItem("editorContent");

    const editor = usePlateEditor({
        value: localValue ? JSON.parse(localValue) : document.content,
        plugins: [HeadingPlugin, ItalicPlugin, UnderlinePlugin],
        override: {
            components: {
                blockquote: withProps(PlateElement, {
                    as: "blockquote",
                    className:
                        "mb-4 border-l-4 border-[#d0d7de] pl-4 text-[#636c76]",
                }),
                bold: withProps(PlateLeaf, { as: "strong" }),
                h1: withProps(PlateElement, {
                    as: "h1",
                    className:
                        "mb-4 mt-6 text-3xl font-semibold tracking-tight lg:text-4xl",
                }),
                h2: withProps(PlateElement, {
                    as: "h2",
                    className:
                        "mb-4 mt-6 text-2xl font-semibold tracking-tight",
                }),
                h3: withProps(PlateElement, {
                    as: "h3",
                    className: "mb-4 mt-6 text-xl font-semibold tracking-tight",
                }),
                italic: withProps(PlateLeaf, { as: "em" }),
                p: withProps(PlateElement, {
                    as: "p",
                    className: "mb-4",
                }),
                underline: withProps(PlateLeaf, { as: "u" }),
            },
        },
    });

    useEffect(() => {
        // const handleMessageReceived = (_: any, message: string) =>
        //     setMessage(message);
        //
        // const handleNewConnectionReceived = () => {};
        //
        // events(handleMessageReceived);
    }, []);

    return (
        <div>
            {/*<span>*/}
            {/*    message from signalR:{" "}*/}
            {/*    <span style={{ color: "green" }}>{message}</span>{" "}*/}
            {/*</span>*/}

            {/*<Button onClick={() => newMessage(new Date().toISOString())}>*/}
            {/*    Send Message*/}
            {/*</Button>*/}
            <Plate
                editor={editor}
                onChange={({ value }) => {
                    localStorage.setItem(
                        "editorContent",
                        JSON.stringify(value),
                    );
                }}
            >
                <EditorContainer>
                    <CustomEditor />
                </EditorContainer>
            </Plate>
        </div>
    );
}

const editorContainerVariants = cva(
    "relative w-full cursor-text select-text overflow-y-auto caret-primary selection:bg-brand/25 focus-visible:outline-none [&_.slate-selection-area]:border [&_.slate-selection-area]:border-brand/25 [&_.slate-selection-area]:bg-brand/15",
    {
        defaultVariants: {
            variant: "default",
        },
        variants: {
            variant: {
                default: "h-full",
                demo: "h-[650px]",
                select: cn(
                    "group rounded-md border border-input ring-offset-background focus-within:ring-2 focus-within:ring-ring focus-within:ring-offset-2",
                    "has-[[data-readonly]]:w-fit has-[[data-readonly]]:cursor-default has-[[data-readonly]]:border-transparent has-[[data-readonly]]:focus-within:[box-shadow:none]",
                ),
            },
        },
    },
);

export const EditorContainer = ({
    className,
    variant,
    ...props
}: React.HTMLAttributes<HTMLDivElement> &
    VariantProps<typeof editorContainerVariants>) => {
    const editor = useEditorRef();
    const containerRef = useEditorContainerRef();

    return (
        <div
            id={editor.uid}
            ref={containerRef}
            className={cn(
                "ignore-click-outside/toolbar",
                editorContainerVariants({ variant }),
                className,
            )}
            {...props}
        />
    );
};

EditorContainer.displayName = "EditorContainer";

const editorVariants = cva(
    cn(
        "group/editor",
        "relative w-full cursor-text select-text overflow-x-hidden whitespace-pre-wrap break-words",
        "rounded-md ring-offset-background  focus-visible:outline-none",
        "placeholder:text-muted-foreground/80 [&_[data-slate-placeholder]]:top-[auto_!important] [&_[data-slate-placeholder]]:text-muted-foreground/80 [&_[data-slate-placeholder]]:!opacity-100",
        "[&_strong]:font-bold",
    ),
    {
        defaultVariants: {
            variant: "default",
        },
        variants: {
            disabled: {
                true: "cursor-not-allowed opacity-50",
            },
            focused: {
                true: "ring-2 ring-ring ring-offset-2",
            },
            variant: {
                ai: "w-full px-0 text-base md:text-sm",
                aiChat: "max-h-[min(70vh,320px)] w-full max-w-[700px] overflow-y-auto px-3 py-2 text-base md:text-sm",
                default:
                    "size-full px-16 pb-72 pt-4 text-base sm:px-[max(64px,calc(50%-350px))]",
                demo: "size-full px-16 pb-72 pt-4 text-base sm:px-[max(64px,calc(50%-350px))]",
                fullWidth: "size-full px-16 pb-72 pt-4 text-base sm:px-24",
                none: "",
                select: "px-3 py-2 text-base data-[readonly]:w-fit",
            },
        },
    },
);

export type EditorProps = PlateContentProps &
    VariantProps<typeof editorVariants>;

export const CustomEditor = React.forwardRef<HTMLDivElement, EditorProps>(
    ({ className, disabled, focused, variant, ...props }, ref) => {
        return (
            <PlateContent
                ref={ref}
                className={cn(
                    editorVariants({
                        disabled,
                        focused,
                        variant,
                    }),
                    className,
                )}
                disabled={disabled}
                disableDefaultStyles
                {...props}
            />
        );
    },
);

CustomEditor.displayName = "Editor";
