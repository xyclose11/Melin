import React, { createContext, useContext, useState } from "react";

type GroupSelectionContextType = {
    selectedGroup: string[];
    clearSelection: () => void;
    toggleGroup: (name: string) => void;
};

const GroupSelectionContext = createContext<
    GroupSelectionContextType | undefined
>(undefined);
export const GroupSelectedProvider: React.FC<{
    children: React.ReactNode;
}> = ({ children }) => {
    const [selectedGroup, setSelectedGroups] = useState<string[]>([]);
    const clearSelection = () => {
        setSelectedGroups([]);
    };

    const toggleGroup = (name: string) => {
        setSelectedGroups((prev) =>
            prev.includes(name)
                ? prev.filter((refId) => refId !== name)
                : [...prev, name],
        );
    };

    return (
        <GroupSelectionContext.Provider
            value={{ selectedGroup, clearSelection, toggleGroup }}
        >
            {children}
        </GroupSelectionContext.Provider>
    );
};

export const useGroupSelection = () => {
    const context = useContext(GroupSelectionContext);

    if (!context) {
        throw new Error(
            "useGroupSelection must be used within a GroupSelectionProvider",
        );
    }
    return context;
};
