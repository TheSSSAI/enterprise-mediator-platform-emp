'use client';

import React, { useTransition } from 'react';
import { useForm, useFieldArray } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { ProjectBriefSchema } from '@/lib/schemas'; // Level 0
import { updateProjectBrief } from '@/actions/project.actions'; // Level 3
import type { ProjectDTO } from '@/types/project'; // Level 0
import { z } from 'zod';
import { PlusIcon, TrashIcon, ArrowPathIcon } from '@heroicons/react/24/outline';

type FormData = z.infer<typeof ProjectBriefSchema>;

interface SowExtractionFormProps {
  project: ProjectDTO;
  onSaveSuccess?: () => void;
}

/**
 * SowExtractionForm Component
 * 
 * "Human-in-the-Loop" interface for editing AI-extracted SOW data.
 * Validates data against the ProjectBriefSchema and persists via Server Action.
 */
export function SowExtractionForm({ project, onSaveSuccess }: SowExtractionFormProps) {
  const [isPending, startTransition] = useTransition();
  const [serverError, setServerError] = React.useState<string | null>(null);

  const { register, control, handleSubmit, formState: { errors, isDirty } } = useForm<FormData>({
    resolver: zodResolver(ProjectBriefSchema),
    defaultValues: {
      projectId: project.id,
      title: project.title || '',
      description: project.description || '',
      deliverables: project.deliverables?.length ? project.deliverables : [{ description: '' }],
      timeline: project.timeline || { startDate: '', endDate: '' },
      budget: project.budget || { min: 0, max: 0, currency: 'USD' },
      skills: project.requiredSkills?.join(', ') || ''
    }
  });

  const { fields, append, remove } = useFieldArray({
    control,
    name: 'deliverables'
  });

  const onSubmit = async (data: FormData) => {
    setServerError(null);
    startTransition(async () => {
      try {
        const result = await updateProjectBrief(project.id, data);
        if (result.success) {
          if (onSaveSuccess) onSaveSuccess();
        } else {
          setServerError(result.error || 'Failed to update project brief');
        }
      } catch (err) {
        setServerError('An unexpected error occurred.');
      }
    });
  };

  return (
    <form onSubmit={handleSubmit(onSubmit)} className="space-y-6 bg-white p-6 rounded-lg shadow-sm border border-slate-200">
      <div className="space-y-4">
        {/* Project Title */}
        <div>
          <label htmlFor="title" className="block text-sm font-medium text-slate-700">Project Title</label>
          <input
            {...register('title')}
            type="text"
            id="title"
            className="mt-1 block w-full rounded-md border-slate-300 shadow-sm focus:border-blue-500 focus:ring-blue-500 sm:text-sm p-2 border"
            placeholder="e.g., Cloud Migration Strategy"
          />
          {errors.title && <p className="mt-1 text-xs text-red-500">{errors.title.message}</p>}
        </div>

        {/* Description */}
        <div>
          <label htmlFor="description" className="block text-sm font-medium text-slate-700">Scope Summary</label>
          <textarea
            {...register('description')}
            id="description"
            rows={4}
            className="mt-1 block w-full rounded-md border-slate-300 shadow-sm focus:border-blue-500 focus:ring-blue-500 sm:text-sm p-2 border"
            placeholder="Executive summary of the SOW..."
          />
          {errors.description && <p className="mt-1 text-xs text-red-500">{errors.description.message}</p>}
        </div>

        {/* Deliverables (Dynamic Array) */}
        <div>
          <label className="block text-sm font-medium text-slate-700 mb-2">Key Deliverables</label>
          <div className="space-y-2">
            {fields.map((field, index) => (
              <div key={field.id} className="flex gap-2">
                <input
                  {...register(`deliverables.${index}.description` as const)}
                  className="block w-full rounded-md border-slate-300 shadow-sm focus:border-blue-500 focus:ring-blue-500 sm:text-sm p-2 border"
                  placeholder={`Deliverable ${index + 1}`}
                />
                <button
                  type="button"
                  onClick={() => remove(index)}
                  className="p-2 text-slate-400 hover:text-red-500"
                  aria-label="Remove deliverable"
                >
                  <TrashIcon className="h-5 w-5" />
                </button>
              </div>
            ))}
            <button
              type="button"
              onClick={() => append({ description: '' })}
              className="mt-2 inline-flex items-center text-xs font-medium text-blue-600 hover:text-blue-800"
            >
              <PlusIcon className="h-4 w-4 mr-1" /> Add Deliverable
            </button>
          </div>
          {errors.deliverables && <p className="mt-1 text-xs text-red-500">{errors.deliverables.message}</p>}
        </div>

        {/* Skills */}
        <div>
          <label htmlFor="skills" className="block text-sm font-medium text-slate-700">Required Skills</label>
          <input
            {...register('skills')}
            type="text"
            id="skills"
            className="mt-1 block w-full rounded-md border-slate-300 shadow-sm focus:border-blue-500 focus:ring-blue-500 sm:text-sm p-2 border"
            placeholder="e.g., React, AWS, Python (Comma separated)"
          />
          <p className="mt-1 text-xs text-slate-500">Separate skills with commas.</p>
          {errors.skills && <p className="mt-1 text-xs text-red-500">{errors.skills.message}</p>}
        </div>

        {/* Timeline */}
        <div className="grid grid-cols-2 gap-4">
          <div>
            <label htmlFor="startDate" className="block text-sm font-medium text-slate-700">Start Date</label>
            <input
              {...register('timeline.startDate')}
              type="date"
              id="startDate"
              className="mt-1 block w-full rounded-md border-slate-300 shadow-sm focus:border-blue-500 focus:ring-blue-500 sm:text-sm p-2 border"
            />
          </div>
          <div>
            <label htmlFor="endDate" className="block text-sm font-medium text-slate-700">End Date</label>
            <input
              {...register('timeline.endDate')}
              type="date"
              id="endDate"
              className="mt-1 block w-full rounded-md border-slate-300 shadow-sm focus:border-blue-500 focus:ring-blue-500 sm:text-sm p-2 border"
            />
          </div>
        </div>
      </div>

      {serverError && (
        <div className="p-3 bg-red-50 border border-red-200 rounded text-sm text-red-600">
          {serverError}
        </div>
      )}

      <div className="pt-4 flex justify-end gap-3">
        <button
          type="button"
          className="px-4 py-2 border border-slate-300 rounded-md shadow-sm text-sm font-medium text-slate-700 bg-white hover:bg-slate-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500"
          disabled={isPending}
        >
          Cancel
        </button>
        <button
          type="submit"
          className="inline-flex items-center px-4 py-2 border border-transparent rounded-md shadow-sm text-sm font-medium text-white bg-blue-600 hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500 disabled:opacity-50 disabled:cursor-not-allowed"
          disabled={isPending || !isDirty}
        >
          {isPending && <ArrowPathIcon className="h-4 w-4 mr-2 animate-spin" />}
          {isPending ? 'Saving...' : 'Save & Finalize Brief'}
        </button>
      </div>
    </form>
  );
}