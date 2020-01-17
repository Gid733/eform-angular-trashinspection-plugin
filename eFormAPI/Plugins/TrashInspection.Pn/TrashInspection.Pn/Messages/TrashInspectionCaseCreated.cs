/*
The MIT License (MIT)

Copyright (c) 2007 - 2019 Microting A/S

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using Microting.eFormTrashInspectionBase.Infrastructure.Data.Entities;
using TrashInspection.Pn.Infrastructure.Models;

namespace TrashInspection.Pn.Messages
{
    public class TrashInspectionCaseCreated
    {
        public int TemplateId { get; protected set; }
        public int TrashInspectionCaseId { get; protected set; }
        public TrashInspectionModel TrashInspectionModel { get; protected set; }
        public Fraction Fraction  { get; protected set; }
        public Segment Segment  { get; protected set; }

        public TrashInspectionCaseCreated(int templateId, int trashInspectionCaseId,
            TrashInspectionModel trashInspectionModel, Segment segment, Fraction fraction)
        {
            TemplateId = templateId;
            TrashInspectionCaseId = trashInspectionCaseId;
            TrashInspectionModel = trashInspectionModel;
            Segment = segment;
            Fraction = fraction;
        }
    }
}