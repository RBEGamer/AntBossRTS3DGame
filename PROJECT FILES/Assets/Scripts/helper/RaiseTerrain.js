 //------------------------------//
 //  Terrrain_RaiseHeight.js     //
 //  Written by Alucard Jay      //
 //  12/30/2013                  //
 //------------------------------//
 
 #pragma strict
 
 
 #if UNITY_EDITOR
 
 @ContextMenu( "Raise Terrain Heightmap" )
 
 function RaiseTerrainHeightmap() 
 {
     RaiseHeights();
 }
 
 #endif
 
 
 public var myTerrain : Terrain;
 private var terrainData : TerrainData;
 private var heightmapWidth : int;
 private var heightmapHeight : int;
 private var heightmapData : float[,];
 
 public var raiseHeightInUnits : float = 20.0;
 
 
 function RaiseHeights() 
 {
     // - GetTerrainData -
     
     if ( !myTerrain )
     {
         myTerrain = Terrain.activeTerrain;
     }
     
     terrainData = myTerrain.terrainData;
     heightmapWidth = myTerrain.terrainData.heightmapWidth;
     heightmapHeight = myTerrain.terrainData.heightmapHeight;
     
     // --
     
     // store old heightmap data
     heightmapData = terrainData.GetHeights( 0, 0, heightmapWidth, heightmapHeight );
     
     var terrainHeight : float = terrainData.size.y;
     
     // --
     
     var y : int = 0;
     var x : int = 0;
     
     // raise heights
     for ( y = 0; y < heightmapHeight; y ++ )
     {
         for ( x = 0; x < heightmapWidth; x ++ )
         {
             var newHeight : float = Mathf.Clamp01( heightmapData[ y, x ] + ( raiseHeightInUnits / terrainHeight ) );
             
             heightmapData[ y, x ] = newHeight;
         }
     }
     
     terrainData.SetHeights( 0, 0, heightmapData );
     
     Debug.Log( "RaiseHeights() completed" );
 }