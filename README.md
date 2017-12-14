# Voronoi-Delaunay

This is an implementation and visualization of [Voronoi diagram](https://en.wikipedia.org/wiki/Voronoi_diagram) and [Delaunay triangulation](https://en.wikipedia.org/wiki/Delaunay_triangulation) in C#.

In this implementation, Voronoi diagram is obtained from generating its dual, Delaunay triangulation. And Delaunay triangulation is derived in a very simple manner, Bowyer-Watson algorithm. The most naïve implementation of Bowyer-Watson algorithm takes O(n^2) time. But I referenced [a JavaScript implementation](https://github.com/ironwallaby/delaunay) of [@ironwallaby](https://github.com/ironwallaby) to improve the time complexity by sorting the points by x-coordinate and using an open list and a closed list to store Delaunay triangles in each insertion of point.

For Deriving Voronoi diagram, I used a data structure of storing each point with all the triangles incident on it. Thus, I can directly find the 2 adjacent triangles of a certain Delaunay edge and connect the centers of their circumcircles. As Delaunay triangulation is a planar graph, the procedure of finding adjacent triangles takes constant time and the total time of generating Voronoi diagram is O(n).

![voronoi-delaunay](/voronoi-delaunay.bmp)
