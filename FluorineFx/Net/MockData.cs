﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluorineFx.Net
{
  public static class MockData
  {
    public static byte[][] MockbyteArrays = new byte[][]
                                               {
                                                 new byte[]{67,0,0,0,0,5,156,19,0,9,82,66,71,95,95,53,57,52,51,0,0,1,252,0,0,0,0,0,0,0,0,3,0,0,1,179,0,7,67,97,98,105,110,101,116,16,0,44,73,71,84,46,83,66,46,77,77,82,46,82,84,77,80,67,108,105,101,110,116,46,83,105,109,112,108,101,83,104,97,114,101,100,79,98,106,101,99,116,83,108,111,116,0,4,68,97,116,97,16,0,42,73,71,84,46,83,66,46,77,77,82,46,66,117,115,105,110,101,115,115,69,110,116,105,116,105,101,115,46,67,97,98,105,110,101,116,195,80,114,111,102,105,108,101,0,5,69,103,109,73,100,2,0,8,82,66,71,95,53,57,52,51,0,13,77,97,99,104,105,110,101,78,117,109,98,101,114,2,0,5,50,48,57,52,50,0,12,83,101,114,105,97,108,78,117,109,98,101,114,2,0,6,57,50,48,57,52,50,0,8,76,111,99,97,116,105,111,110,2,0,6,69,69,50,50,50,53,0,12,68,101,102,97,117,108,116,84,104,101,109,101,5,0,9,66,97,115,101,68,101,110,111,109,0,64,143,64,0,0,0,0,0,0,12,66,195,97,115,101,67,117,114,114,101,110,99,121,2,0,3,85,83,68,0,4,65,114,101,97,5,0,4,90,111,110,101,2,0,4,69,69,50,50,0,4,66,97,110,107,2,0,2,50,53,0,8,80,111,115,105,116,105,111,110,2,0,1,53,0,5,83,116,121,108,101,2,0,11,71,50,83,95,117,112,82,105,103,104,116,0,14,84,105,109,101,90,111,110,101,79,102,102,115,101,116,2,0,6,45,48,55,58,48,48,0,6,76,111,99,97,108,101,2,0,5,101,110,95,85,83,0,6,86,101,195,110,100,111,114,2,0,3,73,71,84,0,8,80,108,97,116,102,111,114,109,2,0,3,65,86,80,0,13,82,101,108,101,97,115,101,78,117,109,98,101,114,2,0,6,65,86,80,48,49,52,0,0,9,0,10,76,97,115,116,85,112,100,97,116,101,11,66,115,159,231,83,177,80,0,0,0,0,0,9,3,0,0,3,3,0,4,85,115,101,114,16,0,44,73,71,84,46,83,66,46,77,77,82,46,82,84,77,80,67,108,105,101,110,116,46,83,105,109,112,108,101,83,104,97,114,101,100,79,195,98,106,101,99,116,83,108,111,116,0,4,68,97,116,97,16,0,35,73,71,84,46,83,66,46,77,77,82,46,66,117,115,105,110,101,115,115,69,110,116,105,116,105,101,115,46,83,101,115,115,105,111,110,0,3,65,103,101,5,0,11,65,110,110,105,118,101,114,115,97,114,121,5,0,8,66,105,114,116,104,100,97,121,5,0,9,67,97,114,100,67,108,97,115,115,2,0,0,0,6,67,97,114,100,73,100,5,0,17,68,117,112,108,105,99,97,116,101,76,111,99,97,116,105,111,110,2,0,195,0,0,5,69,103,109,73,100,2,0,0,0,19,69,103,109,80,111,105,110,116,80,108,97,121,69,110,97,98,108,101,100,5,0,20,69,103,109,88,116,114,97,67,114,101,100,105,116,69,110,97,98,108,101,100,5,0,9,70,105,114,115,116,78,97,109,101,2,0,0,0,14,70,114,101,101,80,108,97,121,65,99,116,105,118,101,5,0,17,71,105,102,116,80,111,105,110,116,115,69,110,97,98,108,101,100,5,0,6,72,97,115,80,73,78,5,0,11,73,115,65,98,97,110,100,111,110,101,195,100,5,0,13,73,115,65,110,110,105,118,101,114,115,97,114,121,5,0,8,73,115,66,97,110,110,101,100,5,0,10,73,115,66,105,114,116,104,100,97,121,5,0,11,73,115,68,117,112,108,105,99,97,116,101,5,0,11,73,115,80,73,78,76,111,99,107,101,100,5,0,12,73,115,83,101,108,102,66,97,110,110,101,100,5,0,8,76,97,115,116,78,97,109,101,2,0,0,0,8,76,111,99,97,108,101,73,100,2,0,0,0,22,80,108,97,121,101,114,80,111,105,110,116,80,108,97,121,195,69,110,97,98,108,101,100,5,0,15,80,108,97,121,101,114,83,101,115,115,105,111,110,73,100,0,0,0,0,0,0,0,0,0,0,23,80,108,97,121,101,114,88,116,114,97,67,114,101,100,105,116,69,110,97,98,108,101,100,5,0,21,80,111,105,110,116,66,97,108,97,110,99,101,65,118,97,105,108,97,98,108,101,5,0,17,80,111,105,110,116,66,97,108,97,110,99,101,84,111,116,97,108,5,0,13,80,114,101,102,101,114,114,101,100,78,97,109,101,2,0,0,0,6,82,97,110,107,195,73,100,5,0,8,82,97,110,107,78,97,109,101,2,0,0,0,9,83,101,115,115,105,111,110,73,100,0,0,0,0,0,0,0,0,0,0,13,83,101,115,115,105,111,110,84,121,112,101,73,100,0,0,0,0,0,0,0,0,0,0,13,83,116,97,114,116,68,97,116,101,84,105,109,101,5,0,17,88,116,114,97,67,114,101,100,105,116,66,97,108,97,110,99,101,5,0,16,88,116,114,97,67,114,101,100,105,116,76,105,109,105,116,115,5,0,11,67,111,109,112,66,97,108,97,110,99,101,195,5,0,29,80,111,105,110,116,66,97,108,97,110,99,101,65,118,97,105,108,97,98,108,101,67,117,114,114,101,110,99,121,5,0,8,80,108,97,121,101,114,73,100,5,0,10,72,105,100,101,80,111,105,110,116,115,1,0,0,0,9,0,10,76,97,115,116,85,112,100,97,116,101,11,66,115,159,231,83,177,80,0,0,0,0,0,9,3,0,0,0,192,0,11,85,115,101,114,83,101,115,115,105,111,110,16,0,44,73,71,84,46,83,66,46,77,77,82,46,82,84,77,80,67,108,105,101,110,195,116,46,83,105,109,112,108,101,83,104,97,114,101,100,79,98,106,101,99,116,83,108,111,116,0,4,68,97,116,97,16,0,46,73,71,84,46,83,66,46,77,77,82,46,66,117,115,105,110,101,115,115,69,110,116,105,116,105,101,115,46,85,115,101,114,83,101,115,115,105,111,110,80,114,111,102,105,108,101,0,11,83,101,115,115,105,111,110,84,121,112,101,0,0,0,0,0,0,0,0,0,0,15,67,97,114,100,101,100,83,101,115,115,105,111,110,73,100,0,0,0,0,0,0,0,0,0,0,195,0,9,0,10,76,97,115,116,85,112,100,97,116,101,11,66,115,159,231,83,177,80,0,0,0,0,0,9}, 
                                                 new byte[]{195,0,9,82,66,71,95,95,53,57,53,49,0,0,1,255,0,0,0,0,0,0,0,0,6,0,0,0,19,2,0,16,83,101,110,100,67,108,105,101,110,116,83,116,97,116,117,115}, 
                                                 new byte[]{67,0,0,0,0,0,47,19,0,9,82,66,71,95,95,53,57,52,57,0,0,1,255,0,0,0,0,0,0,0,0,6,0,0,0,19,2,0,16,83,101,110,100,67,108,105,101,110,116,83,116,97,116,117,115}, 
                                                 new byte[]{195,0,9,82,66,71,95,95,53,57,48,52,0,0,2,1,0,0,0,0,0,0,0,0,6,0,0,0,19,2,0,16,83,101,110,100,67,108,105,101,110,116,83,116,97,116,117,115}, 
                                                 new byte[]{195,0,9,82,66,71,95,95,53,57,53,56,0,0,2,5,0,0,0,0,0,0,0,0,6,0,0,0,19,2,0,16,83,101,110,100,67,108,105,101,110,116,83,116,97,116,117,115}, 
                                                 new byte[]{195,0,9,82,66,71,95,95,53,56,52,57,0,0,2,11,0,0,0,0,0,0,0,0,6,0,0,0,19,2,0,16,83,101,110,100,67,108,105,101,110,116,83,116,97,116,117,115}, 
                                                 new byte[]{195,0,9,82,66,71,95,95,53,56,52,54,0,0,2,7,0,0,0,0,0,0,0,0,6,0,0,0,19,2,0,16,83,101,110,100,67,108,105,101,110,116,83,116,97,116,117,115}, 
                                                 new byte[]{195,0,9,82,66,71,95,95,53,57,52,51,0,0,1,253,0,0,0,0,0,0,0,0,6,0,0,0,19,2,0,16,83,101,110,100,67,108,105,101,110,116,83,116,97,116,117,115}, 
                                                 new byte[]{195,0,9,82,66,71,95,95,53,56,52,49,0,0,1,111,0,0,0,0,0,0,0,0,6,0,0,0,19,2,0,16,83,101,110,100,67,108,105,101,110,116,83,116,97,116,117,115}, 
                                                 new byte[]{67,0,0,0,0,3,79,19,0,9,82,66,71,95,95,53,55,54,52,0,0,0,42,0,0,0,0,0,0,0,0,3,0,0,3,51,0,4,85,115,101,114,16,0,44,73,71,84,46,83,66,46,77,77,82,46,82,84,77,80,67,108,105,101,110,116,46,83,105,109,112,108,101,83,104,97,114,101,100,79,98,106,101,99,116,83,108,111,116,0,4,68,97,116,97,16,0,35,73,71,84,46,83,66,46,77,77,82,46,66,117,115,105,110,101,115,115,69,110,116,105,116,105,101,115,46,83,101,115,115,105,111,110,0,3,65,195,103,101,5,0,11,65,110,110,105,118,101,114,115,97,114,121,5,0,8,66,105,114,116,104,100,97,121,5,0,9,67,97,114,100,67,108,97,115,115,2,0,0,0,6,67,97,114,100,73,100,2,0,13,49,50,51,52,53,54,48,50,48,51,53,49,48,0,17,68,117,112,108,105,99,97,116,101,76,111,99,97,116,105,111,110,2,0,0,0,5,69,103,109,73,100,2,0,8,82,66,71,95,53,55,54,52,0,19,69,103,109,80,111,105,110,116,80,108,97,121,69,110,97,98,108,101,100,5,195,0,20,69,103,109,88,116,114,97,67,114,101,100,105,116,69,110,97,98,108,101,100,5,0,9,70,105,114,115,116,78,97,109,101,2,0,5,83,73,77,79,78,0,14,70,114,101,101,80,108,97,121,65,99,116,105,118,101,5,0,17,71,105,102,116,80,111,105,110,116,115,69,110,97,98,108,101,100,5,0,6,72,97,115,80,73,78,5,0,11,73,115,65,98,97,110,100,111,110,101,100,1,0,0,13,73,115,65,110,110,105,118,101,114,115,97,114,121,5,0,8,73,115,66,97,110,110,101,195,100,1,0,0,10,73,115,66,105,114,116,104,100,97,121,5,0,11,73,115,68,117,112,108,105,99,97,116,101,5,0,11,73,115,80,73,78,76,111,99,107,101,100,1,0,0,12,73,115,83,101,108,102,66,97,110,110,101,100,1,0,0,8,76,97,115,116,78,97,109,101,2,0,6,82,79,76,76,69,82,0,8,76,111,99,97,108,101,73,100,2,0,5,101,110,95,85,83,0,22,80,108,97,121,101,114,80,111,105,110,116,80,108,97,121,69,110,97,98,108,101,100,5,0,15,80,108,97,195,121,101,114,83,101,115,115,105,111,110,73,100,0,0,0,0,0,0,0,0,0,0,23,80,108,97,121,101,114,88,116,114,97,67,114,101,100,105,116,69,110,97,98,108,101,100,5,0,21,80,111,105,110,116,66,97,108,97,110,99,101,65,118,97,105,108,97,98,108,101,5,0,17,80,111,105,110,116,66,97,108,97,110,99,101,84,111,116,97,108,5,0,13,80,114,101,102,101,114,114,101,100,78,97,109,101,2,0,5,83,73,77,79,78,0,6,82,97,110,107,73,100,5,0,8,82,97,110,195,107,78,97,109,101,2,0,0,0,9,83,101,115,115,105,111,110,73,100,0,0,0,0,0,0,0,0,0,0,13,83,101,115,115,105,111,110,84,121,112,101,73,100,0,63,240,0,0,0,0,0,0,0,13,83,116,97,114,116,68,97,116,101,84,105,109,101,5,0,17,88,116,114,97,67,114,101,100,105,116,66,97,108,97,110,99,101,5,0,16,88,116,114,97,67,114,101,100,105,116,76,105,109,105,116,115,5,0,11,67,111,109,112,66,97,108,97,110,99,101,5,0,29,80,111,105,110,116,195,66,97,108,97,110,99,101,65,118,97,105,108,97,98,108,101,67,117,114,114,101,110,99,121,5,0,8,80,108,97,121,101,114,73,100,5,0,10,72,105,100,101,80,111,105,110,116,115,1,0,0,0,9,0,10,76,97,115,116,85,112,100,97,116,101,11,66,115,159,231,84,12,208,0,0,0,0,0,9}, 
                                                 new byte[]{67,0,0,0,0,4,136,19,0,9,82,66,71,95,95,53,55,54,52,0,0,0,43,0,0,0,0,0,0,0,0,3,0,0,0,192,0,11,85,115,101,114,83,101,115,115,105,111,110,16,0,44,73,71,84,46,83,66,46,77,77,82,46,82,84,77,80,67,108,105,101,110,116,46,83,105,109,112,108,101,83,104,97,114,101,100,79,98,106,101,99,116,83,108,111,116,0,4,68,97,116,97,16,0,46,73,71,84,46,83,66,46,77,77,82,46,66,117,115,105,110,101,115,115,69,110,116,105,116,105,101,115,46,85,115,101,195,114,83,101,115,115,105,111,110,80,114,111,102,105,108,101,0,11,83,101,115,115,105,111,110,84,121,112,101,0,63,240,0,0,0,0,0,0,0,15,67,97,114,100,101,100,83,101,115,115,105,111,110,73,100,0,65,24,37,12,0,0,0,0,0,0,9,0,10,76,97,115,116,85,112,100,97,116,101,11,66,115,159,231,84,16,64,0,0,0,0,0,9,3,0,0,3,167,0,4,85,115,101,114,16,0,44,73,71,84,46,83,66,46,77,77,82,46,82,84,77,80,67,108,105,101,110,116,46,195,83,105,109,112,108,101,83,104,97,114,101,100,79,98,106,101,99,116,83,108,111,116,0,4,68,97,116,97,16,0,35,73,71,84,46,83,66,46,77,77,82,46,66,117,115,105,110,101,115,115,69,110,116,105,116,105,101,115,46,83,101,115,115,105,111,110,0,3,65,103,101,0,64,69,128,0,0,0,0,0,0,11,65,110,110,105,118,101,114,115,97,114,121,11,66,98,94,162,248,48,0,0,0,0,0,8,66,105,114,116,104,100,97,121,11,193,247,102,251,104,0,0,0,0,0,0,9,67,195,97,114,100,67,108,97,115,115,2,0,0,0,6,67,97,114,100,73,100,2,0,13,49,50,51,52,53,54,48,50,48,51,53,49,48,0,17,68,117,112,108,105,99,97,116,101,76,111,99,97,116,105,111,110,2,0,0,0,5,69,103,109,73,100,2,0,8,82,66,71,95,53,55,54,52,0,19,69,103,109,80,111,105,110,116,80,108,97,121,69,110,97,98,108,101,100,1,1,0,20,69,103,109,88,116,114,97,67,114,101,100,105,116,69,110,97,98,108,101,100,1,1,0,9,70,105,114,115,195,116,78,97,109,101,2,0,5,83,73,77,79,78,0,14,70,114,101,101,80,108,97,121,65,99,116,105,118,101,1,0,0,17,71,105,102,116,80,111,105,110,116,115,69,110,97,98,108,101,100,1,0,0,6,72,97,115,80,73,78,1,1,0,11,73,115,65,98,97,110,100,111,110,101,100,1,0,0,13,73,115,65,110,110,105,118,101,114,115,97,114,121,1,0,0,8,73,115,66,97,110,110,101,100,1,0,0,10,73,115,66,105,114,116,104,100,97,121,1,0,0,11,73,115,68,117,112,108,195,105,99,97,116,101,1,0,0,11,73,115,80,73,78,76,111,99,107,101,100,1,0,0,12,73,115,83,101,108,102,66,97,110,110,101,100,1,0,0,8,76,97,115,116,78,97,109,101,2,0,6,82,79,76,76,69,82,0,8,76,111,99,97,108,101,73,100,2,0,5,101,110,95,85,83,0,22,80,108,97,121,101,114,80,111,105,110,116,80,108,97,121,69,110,97,98,108,101,100,1,1,0,15,80,108,97,121,101,114,83,101,115,115,105,111,110,73,100,0,65,18,1,12,0,0,0,0,0,195,23,80,108,97,121,101,114,88,116,114,97,67,114,101,100,105,116,69,110,97,98,108,101,100,1,1,0,21,80,111,105,110,116,66,97,108,97,110,99,101,65,118,97,105,108,97,98,108,101,0,64,205,1,0,0,0,0,0,0,17,80,111,105,110,116,66,97,108,97,110,99,101,84,111,116,97,108,0,64,205,1,0,0,0,0,0,0,13,80,114,101,102,101,114,114,101,100,78,97,109,101,2,0,5,83,73,77,79,78,0,6,82,97,110,107,73,100,0,0,0,0,0,0,0,0,0,0,8,195,82,97,110,107,78,97,109,101,2,0,0,0,9,83,101,115,115,105,111,110,73,100,0,65,24,37,12,0,0,0,0,0,13,83,101,115,115,105,111,110,84,121,112,101,73,100,0,63,240,0,0,0,0,0,0,0,13,83,116,97,114,116,68,97,116,101,84,105,109,101,11,66,115,159,231,84,16,16,0,0,0,0,17,88,116,114,97,67,114,101,100,105,116,66,97,108,97,110,99,101,0,0,0,0,0,0,0,0,0,0,16,88,116,114,97,67,114,101,100,105,116,76,105,109,105,116,115,0,195,0,0,0,0,0,0,0,0,0,11,67,111,109,112,66,97,108,97,110,99,101,0,65,2,32,160,0,0,0,0,0,29,80,111,105,110,116,66,97,108,97,110,99,101,65,118,97,105,108,97,98,108,101,67,117,114,114,101,110,99,121,0,64,205,1,0,0,0,0,0,0,8,80,108,97,121,101,114,73,100,2,0,10,50,48,48,48,48,48,51,53,49,48,0,10,72,105,100,101,80,111,105,110,116,115,1,0,0,0,9,0,10,76,97,115,116,85,112,100,97,116,101,11,66,115,159,231,84,195,16,64,0,0,0,0,0,9}, 
                                               };
  }
}