#ifndef _OCTREE_H
#define _OCTREE_H

#pragma warning (disable: 4901)

/// <summary>
/// One octree node.
/// </summary>
struct OctreeNode
{
    /// <summary>
    /// The color of the node.
    /// </summary>
    UINT32 Color;
    /// <summary>
    /// Color count.
    /// </summary>
    UINT32 Count;
};

#endif
